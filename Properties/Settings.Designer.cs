// VBConversions Note: VB project level imports
using System.Collections.Generic;
using System;
using System.Linq;
using System.Drawing;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Data;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using System.Collections;
using System.Windows.Forms;
// End of VB project level imports

using System.Threading;

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



namespace MasterDetailSample
{
	namespace My
	{
		
		[global::System.Runtime.CompilerServices.CompilerGeneratedAttribute(), 
		global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0"), 
		global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase
		{
			
			private static Settings defaultInstance = (My.Settings) (global::System.Configuration.ApplicationSettingsBase.Synchronized(new My.Settings()));
			
#region My.Settings Auto-Save Functionality
#if _MyType
			private static bool addedHandler;
			
			private static object addedHandlerLockObject = new object();
			
			[global::System.Diagnostics.DebuggerNonUserCodeAttribute(), global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]private static void AutoSaveSettings(global::System.Object sender, global::System.EventArgs e)
			{
				if ((new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase()).SaveMySettingsOnExit)
				{
					My.Settings.Default.Save();
				}
			}
#endif
#endregion
			
public static Settings Default
			{
				get
				{
					
#if _MyType
					if (!addedHandler)
					{
						lock(addedHandlerLockObject)
						{
							if (!addedHandler)
							{
								(new Microsoft.VisualBasic.ApplicationServices.WindowsFormsApplicationBase).Shutdown += AutoSaveSettings;
								addedHandler = true;
							}
						}
					}
#endif
					return defaultInstance;
				}
			}
			
[global::System.Configuration.ApplicationScopedSettingAttribute(), 
global::System.Diagnostics.DebuggerNonUserCodeAttribute(), 
global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString), 
global::System.Configuration.DefaultSettingValueAttribute("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=|DataDirectory|\\nwind.mdb")]public string nwindConnectionString
			{
				get
				{
					return System.Convert.ToString(this["nwindConnectionString"]);
				}
			}
		}
	}
	
	namespace My
	{
		
		[global::Microsoft.VisualBasic.HideModuleNameAttribute(), 
		global::System.Diagnostics.DebuggerNonUserCodeAttribute(), 
		global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]internal sealed class MySettingsProperty
		{
			
[global::System.ComponentModel.Design.HelpKeywordAttribute("My.Settings")]internal static global::MasterDetailSample.My.Settings Settings
			{
				get
				{
					return global::MasterDetailSample.My.Settings.Default;
				}
			}
		}
	}
	
}
