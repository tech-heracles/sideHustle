using System;
using System.Runtime.Serialization;

[Serializable] 
public class mySessionNewException : Exception
{
    private string mesazhKlienti;
    private string mesazhLogu;

    public string MesazhLogu
    {
        get
        {
            return mesazhLogu;
        }
        set
        {
            if (mesazhLogu == value)
                return;
            mesazhLogu = value;
        }
    }
    public string MesazhKlienti
    {
        get
        {
            return mesazhKlienti;
        }
        set
        {
            if (mesazhKlienti == value)
                return;
            mesazhKlienti = value;
        }
    }
    protected mySessionNewException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        if (info == null)
            throw new ArgumentNullException("info");
        this.mesazhKlienti = (string)info.GetValue("mesazhKlienti", typeof(string));
        this.mesazhLogu = (string)info.GetValue("mesazhLogu", typeof(string));        
    }
    public mySessionNewException()
    {        
        this.mesazhKlienti = DateTime.Now + "Gabim i panjohur";
        this.mesazhLogu = this.InnerException.Message;
    }
    public mySessionNewException(string mesazhKlienti)
    {
        this.mesazhKlienti = mesazhKlienti;        
    }
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {        
        if (info == null)
            throw new ArgumentNullException("info");
        info.AddValue("mesazhKlienti", this.mesazhKlienti);
        info.AddValue("mesazhLogu", this.mesazhLogu);
        base.GetObjectData(info, context);
    }
}