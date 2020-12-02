package kabam.rotmg.ui.welcomeview {
import kabam.rotmg.account.core.Account;
import kabam.rotmg.account.core.signals.LogoutSignal;
import kabam.rotmg.account.core.signals.UpdateAccountInfoSignal;
import kabam.rotmg.account.core.view.AccountInfoView;
import kabam.rotmg.account.web.view.WebLoginDialog;
import kabam.rotmg.account.web.view.WebRegisterDialog;
import kabam.rotmg.dialogs.control.OpenDialogSignal;
import kabam.rotmg.ui.signals.EnterGameSignal;

import robotlegs.bender.bundles.mvcs.Mediator;

public class WelcomeScreenMediator extends Mediator {

    [Inject]
    public var view:WelcomeScreen;
    [Inject]
    public var openDialog:OpenDialogSignal;
    [Inject]
    public var logoutSignal:LogoutSignal;
    [Inject]
    public var enterGame:EnterGameSignal;

    public function WelcomeScreenMediator() {
        super();
    }

    override public function initialize():void {
        this.view.registerSignal.add(onRegister);
        this.view.loginSignal.add(onLogin);
        this.view.playSignal.add(onPlay);
    }

    private function onPlay():void {
        this.enterGame.dispatch();
    }
    private function onRegister():void {
        this.openDialog.dispatch(new WebRegisterDialog());
    }
    private function onLogin():void {
        if (!this.view.isLoggedIn) {
            this.openDialog.dispatch(new WebLoginDialog());
        } else {
            this.logoutSignal.dispatch();
            this.view.setInfo("", false);
        }
    }
}
}
