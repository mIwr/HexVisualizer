using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System.IO;
using System.Collections.Generic;
using System.Threading;


namespace VsixHexVisualizer
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(VisualizerPackage.PackageGuidString)]    
    [ProvideAutoLoad(VSConstants.UICONTEXT.NoSolution_string, PackageAutoLoadFlags.BackgroundLoad)]
    public sealed class VisualizerPackage : AsyncPackage
    {
        /// <summary>
        /// VisualizerPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "59EC5E5E-F3F3-4C8D-8DBE-4117AC415EC1";

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualizerPackage"/> class.
        /// </summary>
        public VisualizerPackage()
        {
        }

        protected override async System.Threading.Tasks.Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {            
            //mostly gotten from https://github.com/visualstudioextensibility/VSX-Samples/tree/master/VSIXDebuggerVisualizer
            var PAYLOAD_FILE_NAMES = new List<string>() { "HexVisualizer.dll", "Be.Windows.Forms.HexBox.dll" };

            string sourceFolderFullName;
            string destinationFolderFullName;
            IVsShell shell;
            object documentsFolderFullNameObject;
            string documentsFolderFullName;

            try
            {
                await base.InitializeAsync(cancellationToken, progress);

                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

                // The Visualizer dll is in the same folder than the package because its project is added as reference to this project,
                // so it is included inside the .vsix file. We only need to deploy it to the correct destination folder.
                sourceFolderFullName = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

                // Get the destination folder for visualizers
                shell = await base.GetServiceAsync(typeof(SVsShell)) as IVsShell;
                if (shell == null)
                {
                    return;
                }
                shell.GetProperty((int)__VSSPROPID2.VSSPROPID_VisualStudioDir, out documentsFolderFullNameObject);
                documentsFolderFullName = documentsFolderFullNameObject.ToString();
                destinationFolderFullName = Path.Combine(documentsFolderFullName, "Visualizers");

                foreach (var payload in PAYLOAD_FILE_NAMES)
                {
                    var sourceFileFullName = Path.Combine(sourceFolderFullName, payload);
                    var destinationFileFullName = Path.Combine(destinationFolderFullName, payload);

                    CopyFileIfNewerVersion(sourceFileFullName, destinationFileFullName);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void CopyFileIfNewerVersion(string sourceFileFullName, string destinationFileFullName)
        {
            FileVersionInfo destinationFileVersionInfo;
            FileVersionInfo sourceFileVersionInfo;
            bool copy = false;

            if (File.Exists(destinationFileFullName))
            {
                sourceFileVersionInfo = FileVersionInfo.GetVersionInfo(sourceFileFullName);
                destinationFileVersionInfo = FileVersionInfo.GetVersionInfo(destinationFileFullName);
                if (sourceFileVersionInfo.FileMajorPart > destinationFileVersionInfo.FileMajorPart)
                {
                    copy = true;
                }
                else if (sourceFileVersionInfo.FileMajorPart == destinationFileVersionInfo.FileMajorPart
                   && sourceFileVersionInfo.FileMinorPart > destinationFileVersionInfo.FileMinorPart)
                {
                    copy = true;
                }
            }
            else
            {
                // First time
                copy = true;
            }

            if (copy)
            {
                File.Copy(sourceFileFullName, destinationFileFullName, true);
            }
        }

    }
}
