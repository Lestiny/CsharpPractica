using System.Text.Json;
using PhotoGallery.Models;

namespace PhotoGallery.Data;

public sealed class JsonPhotoStore : IPhotoStore
{
    private readonly object _sync = new();
    private readonly string _filePath;
    private List<Photo> _photos = new();
    private int _nextId = 1;

    public JsonPhotoStore(IWebHostEnvironment env)
    {
        var dataDir = Path.Combine(env.ContentRootPath, "App_Data");
        Directory.CreateDirectory(dataDir);
        _filePath = Path.Combine(dataDir, "photos.json");
        Load();
    }

    public IReadOnlyList<Photo> GetAll()
    {
        lock (_sync)
        {
            return _photos.ToArray();
        }
    }

    public Photo? GetById(int id)
    {
        lock (_sync)
        {
            return _photos.FirstOrDefault(p => p.Id == id);
        }
    }

    public Photo Add(string title, string url)
    {
        lock (_sync)
        {
            var photo = new Photo
            {
                Id = _nextId++,
                Title = title.Trim(),
                Url = url.Trim(),
                DateUploaded = DateTime.UtcNow
            };

            _photos.Insert(0, photo);
            Save();
            return photo;
        }
    }

    public bool Delete(int id)
    {
        lock (_sync)
        {
            var idx = _photos.FindIndex(p => p.Id == id);
            if (idx < 0)
            {
                return false;
            }

            _photos.RemoveAt(idx);
            Save();
            return true;
        }
    }

    private void Load()
    {
        lock (_sync)
        {
            if (!File.Exists(_filePath))
            {
                _photos = new List<Photo>();
                _nextId = 1;
                return;
            }

            try
            {
                var json = File.ReadAllText(_filePath);
                var data = JsonSerializer.Deserialize<List<Photo>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _photos = data ?? new List<Photo>();
                _nextId = _photos.Count == 0 ? 1 : _photos.Max(p => p.Id) + 1;
            }
            catch
            {
                _photos = new List<Photo>();
                _nextId = 1;
            }
        }
    }

    private void Save()
    {
        var json = JsonSerializer.Serialize(_photos, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(_filePath, json);
    }
}

