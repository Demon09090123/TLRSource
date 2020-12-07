package kabam.rotmg.ui.welcomeview {
import com.company.ui.SimpleText;

import drawing.components.TitleMenuOption;

import drawing.uiAssets.UIComponentHelper;

import flash.display.DisplayObject;

import flash.display.Sprite;
import flash.events.MouseEvent;

import kabam.rotmg.account.core.view.AccountInfoView;

import org.osflash.signals.Signal;
import org.osflash.signals.natives.NativeMappedSignal;

public class WelcomeScreen extends Sprite implements AccountInfoView {

    private const LOGIN:String = "LOGIN";
    private const LOGOUT:String = "LOGOUT";
    private const REGISTER:String = "REGISTER";
    private const PLAY:String = "PLAY";
    private const SERVER:String = "SERVER";

    private const WIDTH:int = 800;
    private const HEIGHT:int = 600;

    public var isLoggedIn:Boolean = false;

    private var loginBtn:TitleMenuOption;
    private var registerBtn:TitleMenuOption;
    private var playBtn:TitleMenuOption;
    private var serverBtn:TitleMenuOption;

    private var titleText:SimpleText;

    public var loginSignal:Signal;
    public var registerSignal:Signal;
    public var playSignal:Signal;

    public function WelcomeScreen() {

        this.makeButtons();
        this.makeText();
        this.setupSignal();
    }

    public function setInfo(isRegistered:Boolean):void {
        while(numChildren) {
            removeChildAt(0);
        }

        addChild(UIComponentHelper._welcomeScreen);
        this.titleText.y = 20;
        this.titleText.x = (this.WIDTH - this.titleText.width) / 2;

        addChild(this.titleText);

        if (isRegistered == false) {
            alignDraw(130, this.loginBtn, this.registerBtn, this.serverBtn, this.playBtn);
            return;
        }

        this.isLoggedIn = true;
        this.playBtn.activate();
        this.serverBtn.activate();
        this.loginBtn.setText(LOGOUT);

        alignDraw(130, this.loginBtn, this.serverBtn, this.playBtn);
    }

    private function alignDraw(xOffset:int, ... ui):void {
        var yOffset:int = xOffset;

        for each(var d:DisplayObject in ui) {
            d.x = (this.WIDTH - d.width) / 2;
            d.y = yOffset;
            yOffset += (d.height + 5);

            addChild(d);
        }
    }

    private function setupSignal():void {
        this.loginSignal = new NativeMappedSignal(this.loginBtn, MouseEvent.CLICK);
        this.registerSignal = new NativeMappedSignal(this.registerBtn, MouseEvent.CLICK);
        this.playSignal = new NativeMappedSignal(this.playBtn, MouseEvent.CLICK);
    }

    private function makeText() :void {

        this.titleText = new SimpleText(38, 0xFFFFFF).setBold(true);
        this.titleText.text = "Last Realm";
        this.titleText.updateMetrics();
        this.titleText.addOutline();
    }

    private function makeButtons():void {
        this.loginBtn = new TitleMenuOption(this.LOGIN, 28, false, true);
        this.registerBtn = new TitleMenuOption(this.REGISTER, 28, false, true);
        this.playBtn = new TitleMenuOption(this.PLAY, 28, false, true);
        this.serverBtn = new TitleMenuOption(this.SERVER, 28, false, true);

        this.loginBtn.addOutline(2);
        this.registerBtn.addOutline(2);
        this.playBtn.addOutline(2);
        this.serverBtn.addOutline(2);

        this.playBtn.deactivate();
        this.serverBtn.deactivate();
    }
}
}
