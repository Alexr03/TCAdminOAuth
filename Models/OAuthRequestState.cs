namespace TCAdminOAuth.Models
{
    public class OAuthRequestState
    {
        public OAuthRequestLoginState RequestLoginState { get; set; }

        public OAuthProvider Provider { get; set; }
        
        public int UserId { get; set; }
    }

    public enum OAuthRequestLoginState
    {
        Login,
        Link
    }
}