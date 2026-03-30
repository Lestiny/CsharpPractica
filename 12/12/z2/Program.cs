using System;

interface IHttpRequest
{
    string GetHeaders();
}

class BasicHttpRequest : IHttpRequest
{
    public string GetHeaders()
    {
        return "";
    }
}

class HeaderDecorator : IHttpRequest
{
    protected IHttpRequest req;

    public HeaderDecorator(IHttpRequest r)
    {
        req = r;
    }

    public virtual string GetHeaders()
    {
        return req.GetHeaders();
    }
}

class AuthHeaderDecorator : HeaderDecorator
{
    public AuthHeaderDecorator(IHttpRequest r) : base(r) { }

    public override string GetHeaders()
    {
        return req.GetHeaders() + "auth\n";
    }
}

class ContentTypeDecorator : HeaderDecorator
{
    public ContentTypeDecorator(IHttpRequest r) : base(r) { }

    public override string GetHeaders()
    {
        return req.GetHeaders() + "type=json\n";
    }
}

class CustomHeaderDecorator : HeaderDecorator
{
    private string h;

    public CustomHeaderDecorator(IHttpRequest r, string header) : base(r)
    {
        h = header;
    }

    public override string GetHeaders()
    {
        return req.GetHeaders() + h + "\n";
    }
}

class Program
{
    static void Main()
    {
        IHttpRequest r = new BasicHttpRequest();

        r = new AuthHeaderDecorator(r);
        r = new ContentTypeDecorator(r);
        r = new CustomHeaderDecorator(r, "x=1");

        Console.WriteLine(r.GetHeaders());
    }
}
