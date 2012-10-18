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
        private readonly KernelBase _Kernel = new StandardKernel();
        private byte[] _PublicKey;

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
            _Kernel.Load(validModuleAssembiles);

            // Import Assemblys into Caliburn for inspection
            AssemblySource.Instance.AddRange(validModuleAssembiles);

            base.Configure();
        }

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
           return  _Kernel.Get(service);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _Kernel.GetAll(service);
        }
    }
}