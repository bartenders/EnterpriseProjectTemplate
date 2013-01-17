param($installPath, $toolsPath, $package, $project)

# The init script is run: 1) once when package first installed to solution, 2) every time the solution is opened

# This is the version with latest changes affecting the vsixes or license, it is NOT necessarily the most current version, and does not need regular updating.
$currVersion = "7.0.3.0"

# Minimum version number required.  (7.0.0 and 7.0.1 were beta releases.)
$rtwVersion = "7.0.2.0"                       

$extensionId = "DF2012_IdeaBladeDesigner,*"
$extensionVsix = "IdeaBlade.VisualStudio.OM.Designer.11.0.vsix"
$templatesVsix = "IdeaBlade.VisualStudio.TemplateInstaller.vsix"

$registryRoot = $DTE.RegistryRoot
$extensionsTypesPath = "hkcu:\$registryRoot\ExtensionManager\ExtensionTypes"

$edmvsixInstallRequired = $true
$tempvsixInstallRequired = $true
$setupInstallRequired = $true

if (Test-Path $extensionsTypesPath)
{
   $ext = Get-Item -Path $extensionsTypesPath | Select-Object -ExpandProperty Property | Where-Object {$_ -like $extensionId}
   if ($ext -ne $null) {
   
      $extarr = $ext.split(",")
      $extVersion = $extarr[1].replace(".", "") -as [int]

      # 1) 7.0.2 was RTW release - if installed version is prior then a full install is needed.
      # 2) 7.0.3 modified the vsixes only - don't want to install the license if not necessary

      $reqVersion = $rtwVersion.replace(".", "") -as [int]
      $chgVersion = $currVersion.replace(".", "") -as [int]
      
      if ($extVersion -ge $chgVersion) {
         $edmvsixInstallRequired = $false
         $tempvsixInstallRequired = $false
         $setupInstallRequired = $false
      } elseif ($extVersion -eq $reqVersion) {
         $edmvsixInstallRequired = $true
         $tempvsixInstallRequired = $true
         $setupInstallRequired = $false
      }  
     # otherwise install everything   
   }
}

if ($edmvsixInstallRequired -or $tempvsixInstallRequired -or $setupInstallRequired)
{
    $VSPath = [System.IO.Path]::GetDirectoryName($DTE.FileName)
    $installer = [System.IO.Path]::Combine($VSPath, "VSIXInstaller.exe") 
    $extensionVsixPath = [System.IO.Path]::Combine($toolsPath, $extensionVsix)
    $templatesVsixPath = [System.IO.Path]::Combine($toolsPath, $templatesVsix)

    if ($edmvsixInstallRequired) {
      $process = [System.Diagnostics.Process]::Start($installer, "`"$extensionVsixPath`"")
      $process.WaitForExit()
    }
    if ($tempvsixInstallRequired) {
      $process = [System.Diagnostics.Process]::Start($installer, "`"$templatesVsixPath`"")
      $process.WaitForExit()
    }
    if ($edmvsixInstallRequired -or $tempvsixInstallRequired) {
      [System.Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms") | Out-Null
      [Windows.Forms.MessageBox]::Show("You must restart Microsoft Visual Studio in order for the changes to take effect.", 
        "Extensions and Updates", [Windows.Forms.MessageBoxButtons]::OK, 
        [System.Windows.Forms.MessageBoxIcon]::Information) | Out-Null
    }
    
    if ($setupInstallRequired) { 
      $toolsSetup = [System.IO.Path]::Combine($toolsPath, "setup.exe")
      [System.Diagnostics.Process]::Start($toolsSetup)
    }
}
else
{
    { "DevForce 2012 Tools are already installed." }
}
