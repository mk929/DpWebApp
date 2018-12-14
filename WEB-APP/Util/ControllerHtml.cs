using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DpWebApp.Util
{

    public static class ControllerHtml
    {
        /*
         * https://stackoverflow.com/questions/8331041/how-to-use-mvc-3-html-actionlink-inside-c-sharp-code
         */
        // this class from internal TemplateHelpers class in System.Web.Mvc namespace
        private class ViewDataContainer : IViewDataContainer
        {
            public ViewDataContainer(ViewDataDictionary viewData)
            {
                ViewData = viewData;
            }

            public ViewDataDictionary ViewData { get; set; }
        }

        private static HtmlHelper htmlHelper;

        public static HtmlHelper Html(Controller controller)
        {
            if (htmlHelper == null)
            {
                var vdd = new ViewDataDictionary();
                var tdd = new TempDataDictionary();
                var controllerContext = controller.ControllerContext;
                var view = new RazorView(controllerContext, "/", "/", false, null);
                htmlHelper = new HtmlHelper(new ViewContext(controllerContext, view, vdd, tdd, new StringWriter()),
                     new ViewDataContainer(vdd), RouteTable.Routes);
            }
            return htmlHelper;
        }

        public static HtmlHelper Html(Controller controller, object model)
        {
            if (htmlHelper == null || htmlHelper.ViewData.Model == null || !htmlHelper.ViewData.Model.Equals(model))
            {
                var vdd = new ViewDataDictionary();
                vdd.Model = model;
                var tdd = new TempDataDictionary();
                var controllerContext = controller.ControllerContext;
                var view = new RazorView(controllerContext, "/", "/", false, null);
                htmlHelper = new HtmlHelper(new ViewContext(controllerContext, view, vdd, tdd, new StringWriter()),
                     new ViewDataContainer(vdd), RouteTable.Routes);
            }
            return htmlHelper;
        }
    }
}