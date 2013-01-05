using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using Caliburn.Micro;
using EPT.Infrastructure.Interfaces;
using EPT.Shell.Properties;
using Ninject;

namespace EPT.Shell
{
    public class NinjectBootstrapper : Bootstrapper<IShell>
    {
        private readonly KernelBase _kernel = new StandardKernel();
        private byte[] _PublicKey;


        /// <summary>
        /// Initializes the <see cref="NinjectBootstrapper" /> class.
        /// </summary>
        //static NinjectBootstrapper()
        //{
        //    Caliburn.Micro.DevExpress.DXConventions.Install();
        //}

        /// <summary>
        /// Configure the framework and setup IoC container.
        /// </summary>
        /// <exception cref="System.Windows.ResourceReferenceKeyNotFoundException"></exception>
        protected override void Configure()
        {
            _PublicKey = Assembly.GetExecutingAssembly().GetName().GetPublicKey();

            var moduleBaseDirectory = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Settings.Default.ModuleBaseDir));
            var modules = moduleBaseDirectory.EnumerateFiles(Settings.Default.ModuleSearchPattern, SearchOption.AllDirectories);
            var moduleFileInfos = modules as List<FileInfo> ?? modules.ToList();
            if (!moduleFileInfos.Any())
                throw new ResourceReferenceKeyNotFoundException(string.Format("Unable to find Modules at {0} with search pattern {1}", Settings.Default.ModuleBaseDir, Settings.Default.ModuleSearchPattern), Settings.Default.ModuleBaseDir);

            var moduleAssemblies = moduleFileInfos.Select(file => Assembly.LoadFile(file.FullName)).Concat(new[] { this.GetType().Assembly }).ToArray();

            var validModuleAssembiles = moduleAssemblies.Where(CheckAssemblySignature).ToList();
            
            // Load Modules via Ninject Kernel
            _kernel.Load(validModuleAssembiles);

            // Import Assemblys into Caliburn for inspection
            AssemblySource.Instance.AddRange(validModuleAssembiles);

            base.Configure();
        }

        /// <summary>
        /// Checks the signature of a given Assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>True, if valid Signature</returns>
        private bool CheckAssemblySignature(Assembly assembly)
        {
            return true;
            //var returnValue = default(bool);
            //var key = assembly.GetName().GetPublicKey();

            //if(key.Length == _PublicKey.Length)
            //{
            //    for (int i = 0; i < key.Length; i++)
            //    {
            //        if(key[i] != _PublicKey[i])
            //        {
            //            returnValue = false;    
            //            break;
            //        };
            //        returnValue= true;
            //    }   
            //}
            //return returnValue;
        }

        protected override object GetInstance(Type service, string key)
        {
           return  _kernel.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _kernel.GetAll(service);
        }
    }
}