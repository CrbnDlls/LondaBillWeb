using System;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Diagnostics;

namespace LondaBillWeb
{
    /// <summary>
    /// Перехватчик запросов для установки в сессии параметров разрешения экрана клиента
    /// до начала работы на сервере любых aspx страниц.
    /// </summary>
    /// <remarks>Для включения в работу надо добавить в web.config: 
    /// &lt;system.web>
    ///   &lt;httpModules>
    ///        &lt;add name="ScreenResolutionSetterModule" type="Util.ScreenResolutionSetterModule, ModuleName" />
    ///    &lt;/httpModules>
    ///   &lt;httpHandlers>
    ///        &lt;add verb="*" path="ScreenResolutionSetter.axd" type="Util.ScreenResolutionSetterModule, ModuleName" validate="false"/>
    ///   &lt;/httpHandlers>
    /// &lt;/system.web>
    /// </remarks>
    public class ScreenResolutionSetterModule : IHttpModule, IRequiresSessionState, IHttpHandler
    {
        #region IHttpModule Members

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            //очистка не требуется
        }

        /// <summary>
        /// Инициализация
        /// </summary>
        /// <param name="context">Объект HttpApplication, предоставляющий доступ к методам, 
        /// свойствам и событиям, являющимся общими для всех объектов в приложении ASP.NET.</param>
        public void Init(HttpApplication context)
        {
            context.AcquireRequestState += OnAcquireRequestState;
        }

        /// <summary>
        /// Обработчик события достижения ASP.NET текущего состояния (например, состояния сеанса), связанного с текущим запросом.
        /// </summary>
        /// <param name="source">Источник</param>
        /// <param name="e">Данные</param>
        public void OnAcquireRequestState(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            if (context.Session != null &&
                    context.Session["ScreenResolutionW"] == null &&
                        context.Request.HttpMethod == "GET" &&
                // чтоб не зациклиться
                            Path.GetFileName(context.Request.FilePath).ToLower() != "screenresolutionsetter.axd")
            {
                // выдаем getScreenResolution.htm из ресурсов
                context.Response.Write(Properties.Resources.getScreenResolution);
                context.Response.Flush();
                // и привет
                app.CompleteRequest();
                // в getScreenResolution.htm есть скрипт, где после получения разрешения экрана клиента 
                // вызывается XMLHttpRequest на ScreenResolutionSetter.axd, 
                // что приводит в ProcessRequest() этого класса и сохранению разрешения в данных сессии,
                // а потом document.location.reload() для загрузки первоначально запрашиваемого ресурса
                
            }
        }
        #endregion

        #region IHttpHandler Members

        /// <summary>
        /// Обработчик НТТР-запроса, собственно и осуществляющий запись разрешения экрана в данные сессии
        /// </summary>
        /// <param name="context">Объект HttpContext, предоставляющий ссылки на внутренние серверные объекты 
        /// (например, Request, Response, Session и Server), используемые для обслуживания HTTP-запросов. </param>
        /// <remarks>Сюда попадаем из XMLHttpRequest со странички getScreenResolution.htm</remarks>
        public void ProcessRequest(HttpContext context)
        {
            context.Session["ScreenResolutionW"] = context.Request.Params["ScreenResolutionW"];
            context.Session["ScreenResolutionH"] = context.Request.Params["ScreenResolutionH"];
            context.Session["Tmp"] = context.Request.Params["Tmp"];
        }

        /// <summary>
        /// Можно ли повторно использовать созданный экземпляр обработчика IHttpHandler
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }
        #endregion
    }
}
