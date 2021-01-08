namespace EmprestimosJogos.Domain.Core.DTO
{
    public class ContextUserDTO
    {
        public ContextUserDTO() { }

        public ContextUserDTO(string id, string userName, string name, string profile, string originalUserName = null)
        {
            Id = id;
            UserName = userName;
            Name = name;
            Profile = profile;
            IsAuthenticated = true;
            OriginalUserName = originalUserName;
        }

        public ContextUserDTO(string id, string userName)
        {
            Id = id;
            UserName = userName;
            IsAuthenticated = true;
        }

        public string Id { get; set; }

        public string UserName { get; set; }

        public string Name { get; set; }

        public string Profile { get; set; }

        public bool IsAuthenticated { get; set; }

        public bool IsAllowedInHasPermissionPolicyChallenge { get; set; }

        public string OriginalUserName { get; set; }

        public bool HasPermisson(string perfilKey) => Profile?.Contains(perfilKey) ?? false;

        public bool IsFromPerfil(string perfilKey)
        {
            return Profile.Contains(perfilKey);
        }
    }
}
