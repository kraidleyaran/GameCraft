﻿#region Using Statements

using System;

#if MONOMAC
using MonoMac.AppKit;
using MonoMac.Foundation;

#elif __IOS__
using MonoTouch.Foundation;
using MonoTouch.UIKit;
#endif
#endregion

namespace GameCraft
{
	#if __IOS__
	[Register("AppDelegate")]
	class Program : UIApplicationDelegate
	
#else
	static class Program
	#endif
    {
		private static Game1 game;

		internal static void RunGame (string[] args)
		{
		    game = args.Length == 0 ? new Game1() : new Game1(args);
		    game.Run ();
		}

	    /// <summary>
		/// The main entry point for the application.
		/// </summary>
		#if !MONOMAC && !__IOS__		 
        [STAThread]
		#endif
		static void Main (string[] args)
		{
			#if MONOMAC
			NSApplication.Init ();

			using (var p = new NSAutoreleasePool ()) {
				NSApplication.SharedApplication.Delegate = new AppDelegate();
				NSApplication.Main(args);
			}
			#elif __IOS__
			UIApplication.Main(args, null, "AppDelegate");
			#else
			RunGame (args);
			#endif
		}

		#if __IOS__
		public override void FinishedLaunching(UIApplication app)
		{
			RunGame();
		}
		#endif
	}

	#if MONOMAC
	class AppDelegate : NSApplicationDelegate
	{
		public override void FinishedLaunching (MonoMac.Foundation.NSObject notification)
		{
			AppDomain.CurrentDomain.AssemblyResolve += (object sender, ResolveEventArgs a) =>  {
				if (a.Name.StartsWith("MonoMac")) {
					return typeof(MonoMac.AppKit.AppKitFramework).Assembly;
				}
				return null;
			};
			Program.RunGame();
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender)
		{
			return true;
		}
	}  
	#endif
}

