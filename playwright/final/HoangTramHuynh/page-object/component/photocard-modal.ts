import { BaseComponent } from "./base-component";
import { Element } from '../../core/element/element';

export class PhotoCardModalComponent extends BaseComponent {
    private readonly photographerIcon = new Element("//div[@data-testid='photos-route']//div[contains(@class,'photographer')]");
    private readonly photographerNameOnHover = new Element("//div[contains(@class,'photographer')]//div[contains(@class,'infoContainer')]");
    private readonly viewProfileButton = new Element("//div[@data-side='bottom']//a[text()='View profile']");

    async hoverPhotographerIcon(): Promise<void> {
        await this.photographerIcon.shouldBeVisible();
        await this.photographerIcon.hoverOnElement();
    }

    async getPhotographerNameOnHover(): Promise<string> {
        await this.photographerNameOnHover.shouldBeVisible();

        const photographerName = await this.photographerNameOnHover.getText();

        return photographerName.trim();
    }

    async clickViewProfileButton(): Promise<void> {
        await this.viewProfileButton.shouldBeVisible();
        await this.viewProfileButton.clickOnElement();
        await this.waitForPageLoad();
    }
}