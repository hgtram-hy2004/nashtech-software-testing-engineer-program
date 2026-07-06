import { BaseComponent } from './base-component';
import { Element } from '../../core/element/element';
import { BrowserUtils } from '../../core/browser/browser-utils';

export class SideBarComponent extends BaseComponent {
    private readonly accountAvatarButton = new Element("//button[@aria-label='Profile']");
    private readonly viewProfileMenuItem = new Element("//a[contains(@class,'profileLink')]");
    async openAccountMenu(): Promise<void> {
        await this.accountAvatarButton.clickOnElement();
    }

    async clickViewProfile(): Promise<void> {
        await BrowserUtils.waitForTimeout(2000);
        await this.viewProfileMenuItem.clickOnElement();
        await this.waitForPageLoad();
    }

    async goToMyProfilePage(): Promise<void> {
        await this.openAccountMenu();
        await this.clickViewProfile();
    }
}