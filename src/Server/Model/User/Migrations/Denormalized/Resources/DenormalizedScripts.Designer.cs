﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace odec.Server.Model.User.Migrations.Denormalized.Resources {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class DenormalizedScripts {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal DenormalizedScripts() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("odec.CP.Server.Model.User.Migrations.Denormalized.Resources.DenormalizedScripts", typeof(DenormalizedScripts).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to  DECLARE @CurrentMigration [nvarchar](max)
        ///begin tran
        ///
        ///IF object_id(&apos;[dbo].[__MigrationHistory]&apos;) IS NOT NULL
        ///    SELECT @CurrentMigration =
        ///        (SELECT TOP (1) 
        ///        [Project1].[MigrationId] AS [MigrationId]
        ///        FROM ( SELECT 
        ///        [Extent1].[MigrationId] AS [MigrationId]
        ///        FROM [dbo].[__MigrationHistory] AS [Extent1]
        ///        WHERE [Extent1].[ContextKey] = N&apos;odec.Server.Model.User.Migrations.Denormalized.DenormalizedConfiguration&apos;
        ///        )  AS [Project1]
        ///        ORDER BY [Pr [rest of string was truncated]&quot;;.
        /// </summary>
        public static string InitialDenormalized {
            get {
                return ResourceManager.GetString("InitialDenormalized", resourceCulture);
            }
        }
    }
}