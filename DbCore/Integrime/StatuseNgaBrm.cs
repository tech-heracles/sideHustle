namespace DbCore.Integrime
{
    /// <summary>
    /// statuset qe vijne nga BRM
    /// </summary>
    public enum StatuseNgaBrm
    {
        /// <summary>
        /// Payment Is performed successfully
        /// </summary>
        Sukses = 0,
        /// <summary>
        /// Failed for BRM Internal logic reason
        /// </summary>
        Error = 1,
        /// <summary>
        /// Resource exception
        /// </summary>
        ResourceException = 98,
        /// <summary>
        /// General exception
        /// </summary>
        GeneralException = 99
    }
}