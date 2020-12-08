package kabam.rotmg.account.core.view
{
   import kabam.rotmg.account.core.Account;
import kabam.rotmg.account.core.services.GetCharListTask;
import kabam.rotmg.account.core.signals.UpdateAccountInfoSignal;
   import robotlegs.bender.bundles.mvcs.Mediator;
   
   public class AccountInfoMediator extends Mediator
   {
      [Inject]
      public var account:Account;
      
      [Inject]
      public var view:AccountInfoView;
      
      [Inject]
      public var update:UpdateAccountInfoSignal;

      [Inject]
      public var charTask:GetCharListTask;
      
      public function AccountInfoMediator()
      {
         super();
      }
      
      override public function initialize() : void
      {
         this.view.setInfo(this.account.isRegistered());
         this.update.add(this.updateLogin);
      }
      
      override public function destroy() : void
      {
         this.update.remove(this.updateLogin);
      }
      
      private function updateLogin() : void
      {
         this.charTask.start();
         this.view.setInfo(this.account.isRegistered());
      }
   }
}
