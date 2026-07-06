import { LoginPage } from '../page-object/login-page';
import { HomePage } from '../page-object/home-page';
import { PhotoDetailPage } from '../page-object/photo-detail-page';
import { PhotographerProfilePage } from '../page-object/photographer-profile-page';
import { BrowserUtils } from '../core/browser/browser-utils';
import { BASE_URL } from '../constant/url';
import { AccountData } from '../data-object/ui/account';

export class PhotographerProfileWorkflow {
    constructor(
        private readonly loginPage: LoginPage,
        private readonly homePage: HomePage,
        private readonly photoDetailPage: PhotoDetailPage,
        private readonly photographerProfilePage: PhotographerProfilePage
    ) { }

    async login(accountInfo: AccountData): Promise<void> {
        await BrowserUtils.navigateTo(BASE_URL);
        await this.homePage.gotoLogin();
        await this.loginPage.login(accountInfo);
    }

    async openSecondPhotoDetail(): Promise<void> {
        await this.homePage.clickSecondPhoto();
    }

    async goToPhotographerProfileFromPhotoDetail(): Promise<void> {
        await this.photoDetailPage.hoverPhotographerIcon();
        await this.photoDetailPage.clickViewProfileButton();
    }

    async verifyPhotographerProfilePageIsDisplayed(): Promise<void> {
        await this.photographerProfilePage.verifyPhotographerProfilePageIsDisplayed();
    }

    async verifyMoreActionsMenuIsDisplayed(): Promise<void> {
        await this.photographerProfilePage.clickMoreActionsButton();
        await this.photographerProfilePage.verifyMoreActionsMenuIsDisplayed();
    }

    async verifyCommonProfileFieldsAreDisplayed(): Promise<void> {
        await this.photographerProfilePage.verifyCommonProfileFieldsAreDisplayed();
    }
}