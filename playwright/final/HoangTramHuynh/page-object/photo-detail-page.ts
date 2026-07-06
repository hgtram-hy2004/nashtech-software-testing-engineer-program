import { BasePage } from './base-page';

export class PhotoDetailPage extends BasePage {

    async hoverPhotographerIcon(): Promise<void> {
        await this.photoCardModalComponent.hoverPhotographerIcon();
    }

    async getPhotographerNameOnHover(): Promise<string> {
        return await this.photoCardModalComponent.getPhotographerNameOnHover();
    }

    async clickViewProfileButton(): Promise<void> {
        await this.photoCardModalComponent.clickViewProfileButton();
    }
}