using System;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;

namespace android {
	[Service]
	[IntentFilter(new string[] { "com.jamme.jaMMEService" })]
	public class jaMMEService : Service {
		public override IBinder OnBind(Intent intent) {
			return new Binder();
		}
		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId) {
			//update the game with 20fps (1000 / 50)
			jaMME.ServiceRunning = true;
			new Thread(() => {
				while (true) {
					Thread.Sleep(50);
					if (jaMME.KillService) {
						jaMME.KillService = false;
						jaMME.ServiceRunning = false;
						break;
					}
					int jammeFlags = jaMME.frame();
				}
			}).Start();
			return StartCommandResult.Sticky;
		}
		public override void OnDestroy() {
			jaMME.KillService = false;
			jaMME.ServiceRunning = false;
			base.OnDestroy();
		}
	}
}