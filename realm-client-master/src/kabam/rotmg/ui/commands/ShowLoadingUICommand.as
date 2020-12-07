package kabam.rotmg.ui.commands
{
   import com.company.assembleegameclient.screens.AccountLoadingScreen;
import com.company.assembleegameclient.util.AssetLoader;

import kabam.rotmg.core.signals.SetScreenSignal;
import kabam.rotmg.startup.control.StartupSequence;

import robotlegs.bender.framework.api.ILogger;
   
   public class ShowLoadingUICommand
   {
      [Inject]
      public var setScreen:SetScreenSignal;
      
      [Inject]
      public var logger:ILogger;
      
      public function ShowLoadingUICommand()
      {
         super();
      }
      
      public function execute() : void
      {
         this.showLoadingScreen();
      }
      
      private function showLoadingScreen() : void
      {
         this.setScreen.dispatch(new AccountLoadingScreen());
      }
   }
}
