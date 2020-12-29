namespace OpenAi.Api
{
    /// <summary>
    /// Options for authenticating calls to the OpenAi Api
    /// </summary>
    public enum EAuthType
    {
        /// <summary>
        /// The local file looks for a key.txt file located at `~/.openai/key.txt` (Linux/Mac)
        /// or `%USERPROFILE%/.openai/key.txt` (Windows) and extracts the key.
        /// </summary>
        LocalFile = 0,

        /// <summary>
        /// The secret is copied into a field of this scriptable object and used directly
        /// </summary>
        String = 1
    }
}