using System.IO;
using System.Security.AccessControl;

namespace DbCore.IMBUtils.Extensions
{
    public static class DirectoryExtension
    {
        public static void CreateDirIfNotExists(string pathDir)
        {
            if (!Directory.Exists(pathDir))
                Directory.CreateDirectory(pathDir);
        }
        /// <summary>
        /// krijon nje folder sipas pathit dhe userit i jep te drejta fullcontroll
        /// </summary>
        /// <param name="identity">emri i userit i caktojme te drejtat persh everyone, IIS_IUSRS etj</param>
        /// <param name="pathDir">pathi tek i cili do krijohet direktoria</param>
        public static void CreateDirIfNotExistsWithFullRightsForUser(string identity, string pathDir)
        {
            CreateDirIfNotExists(pathDir);
            DirectoryInfo folderImportDir = new DirectoryInfo(pathDir);
            DirectorySecurity directorySecurity = folderImportDir.GetAccessControl();
            FileSystemAccessRule userToHaveRights = new FileSystemAccessRule(identity,
                           FileSystemRights.FullControl,
                           InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                           PropagationFlags.None,
                           AccessControlType.Allow);
            directorySecurity.AddAccessRule(userToHaveRights);
            folderImportDir.SetAccessControl(directorySecurity);
        }
    }
}
