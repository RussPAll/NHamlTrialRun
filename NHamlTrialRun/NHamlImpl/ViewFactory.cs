using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHamlTrialRun.Model;
using NHaml4;
using NHaml4.TemplateResolution;
using System.IO;
using NHaml4.TemplateBase;

namespace NHamlTrialRun.NHamlImpl
{
    public class ViewFactory
    {
        private FileTemplateContentProvider _fileTemplateResolver;
        private TemplateEngine _templateEngine;

        public ViewFactory()
        {
            _fileTemplateResolver = new FileTemplateContentProvider();
            _fileTemplateResolver.AddPathSource(GetBaseTemplatePath());

            _templateEngine = GetEngine();
        }

        private string GetBaseTemplatePath()
        {
            return System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"..\..\", "Views");
        }

        private TemplateEngine GetEngine()
        {
            return NHaml4.Configuration.XmlConfigurator.GetTemplateEngine(_fileTemplateResolver,
                new List<string> { "NHamlTrialRun.Helpers", },
                new List<string> { typeof(Task).Assembly.Location });
        }

        public NHamlView<T> Create<T>(string viewName)
        {
            return (NHamlView<T>)GetCompiledTemplate(viewName, typeof(NHamlView<T>)).CreateTemplate();
        }

        public NHamlLayout CreateLayout(string layoutName)
        {
            return (NHamlLayout)GetCompiledTemplate(layoutName, typeof(NHamlLayout)).CreateTemplate();
        }

        private TemplateFactory GetCompiledTemplate(string templateName, Type type)
        {
            var viewSource = _fileTemplateResolver.GetViewSource(templateName + ".haml");
            return _templateEngine.GetCompiledTemplate(viewSource, type);
        }
    }
}
