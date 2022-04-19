using System;
using System.Collections.Generic;
using System.Web;

namespace CacheLayer
{
    public static class ScopeManager
    {
        public const string LoginNdermarrjePath = "Login_Ndermarrje.aspx";
        public const string ScopeIdKey = "scopeID";
        public const string IdNdermKey = "idNderm";
        public const string IdNdermVitKey = "idVitNdermarrje";
        public const string NewScopeIdKey = "newScopeId";
        public static void RedirectToLoginNdermarrje(this HttpContext context)
        {
            if (context == null) throw new ArgumentNullException("context eshte null");
            context.Response.Redirect(LoginNdermarrjePath);
        }
        public static string GjeneroScopeId()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool IsScopeIdVlefshem(string scopeId)
        {
            if (string.IsNullOrWhiteSpace(scopeId)) return false;
            var lista = GlobalCacheManager.MyAppCache.Get<List<string>>(ScopeIdKey);
            if (lista == null || lista.Count == 0) return false;
            return lista.Contains(scopeId);
        }
        public static void RuajScopeId(string scopeId)
        {
            var lista = GlobalCacheManager.MyAppCache.Get<List<string>>(ScopeIdKey) ?? new List<string>();
            lista.Add(scopeId);
            GlobalCacheManager.MyAppCache.Set(ScopeIdKey, lista);
        }
        
        public static void HiqScopeId(string scopeId)
        {
            if (string.IsNullOrWhiteSpace(scopeId))
                return;
            var lista = GlobalCacheManager.MyAppCache?.Get<List<string>>(ScopeIdKey);
            if (lista == null || lista.Count == 0)
                return;
            if (lista.Contains(scopeId))
                lista.Remove(scopeId);
            GlobalCacheManager.MyAppCache?.Set(ScopeIdKey, lista);

        }
    }
}