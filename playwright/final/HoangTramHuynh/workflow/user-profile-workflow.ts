import { expect } from '@playwright/test';
import { LoginPage } from '../page-object/login-page';
import { HomePage } from '../page-object/home-page';
import { MyProfilePage } from '../page-object/my-profile-page';
import { EditProfilePage } from '../page-object/edit-profile-page';
import { SideBarComponent } from '../page-object/component/side-bar';
import { BrowserUtils } from '../core/browser/browser-utils';
import { BASE_URL } from '../constant/url';
import { AccountData } from '../data-object/ui/account';
import { UserServices } from '../services/user-services';

export class UserProfileWorkflow {
    constructor(
        private readonly loginPage: LoginPage,
        private readonly homePage: HomePage,
        private readonly sideBarComponent: SideBarComponent,
        private readonly myProfilePage: MyProfilePage,
        private readonly editProfilePage: EditProfilePage
    ) { }

    async login(accountInfo: AccountData): Promise<void> {
        await BrowserUtils.navigateTo(BASE_URL);
        await this.homePage.gotoLogin();
        await this.loginPage.login(accountInfo);
    }

    async goToMyProfilePage(): Promise<void> {
        await this.sideBarComponent.goToMyProfilePage();
    }

    async goToEditProfilePage(): Promise<void> {
        await this.goToMyProfilePage();
        await this.myProfilePage.clickEditProfileButton();
    }

    async updateUsername(newUsername: string): Promise<void> {
        await this.goToEditProfilePage();
        await this.editProfilePage.editUsername(newUsername);
        await this.editProfilePage.clickUpdateAccountButton();
        await this.editProfilePage.verifyUpdateAccountSuccessMessageIsDisplayed();
    }

    async goToProfilePage(username: string): Promise<void> {
        await this.myProfilePage.openMyProfilePage(username);
    }

    async verifyUpdatedProfileIsDisplayed(username: string, expectedFullName: string): Promise<void> {
        await this.goToProfilePage(username);
        await this.myProfilePage.verifyProfilePageIsDisplayed();
        await this.myProfilePage.verifyFullNameIsDisplayed(expectedFullName);
    }

    async restoreUsernameByAPI(username: string): Promise<void> {
        const response = await UserServices.updateUsername(username);
        const responseBody = await response.text();
        expect(response.status(), `Restore username by API failed. Body: ${responseBody}`).toBe(200);
    }
}