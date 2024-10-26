namespace Contracts.Services;

public interface ITemplateService<in T> where T : class
{
    Task<string> GetTemplateHtmlAsStringAsync(string viewName, T model);
}
