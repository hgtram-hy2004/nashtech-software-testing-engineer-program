import { test as baseTest, expect } from '../core/fixture/base-fixture'

import { LoginPage } from '../page-object/login-page';
import { HomePage } from '../page-object/home-page';
import { PhotoDetailPage } from '../page-object/photo-detail-page';
import { PhotographerProfilePage } from '../page-object/photographer-profile-page';
import { MyProfilePage } from '../page-object/my-profile-page';
import { EditProfilePage } from '../page-object/edit-profile-page';
import { CollectionPage } from '../page-object/collection-page';
import { SideBarComponent } from '../page-object/component/side-bar';

import { PhotographerProfileWorkflow } from '../workflow/photographer-profile-workflow';
import { UserProfileWorkflow } from '../workflow/user-profile-workflow';
import { CollectionWorkflow } from '../workflow/collection-workflow';

type PageFixtures = {
    loginPage: LoginPage;
    homePage: HomePage;
    photoDetailPage: PhotoDetailPage;
    photographerProfilePage: PhotographerProfilePage;
    myProfilePage: MyProfilePage;
    editProfilePage: EditProfilePage;
    collectionPage: CollectionPage;
    sideBarComponent: SideBarComponent;

    photographerWorkflow: PhotographerProfileWorkflow;
    userProfileWorkflow: UserProfileWorkflow;
    collectionWorkflow: CollectionWorkflow;
};

export const test = baseTest.extend<PageFixtures>({
    loginPage: async ({ }, use) => {
        await use(new LoginPage());
    },

    homePage: async ({ }, use) => {
        await use(new HomePage());
    },

    photoDetailPage: async ({ }, use) => {
        await use(new PhotoDetailPage());
    },

    photographerProfilePage: async ({ }, use) => {
        await use(new PhotographerProfilePage());
    },

    myProfilePage: async ({ }, use) => {
        await use(new MyProfilePage());
    },

    editProfilePage: async ({ }, use) => {
        await use(new EditProfilePage());
    },

    collectionPage: async ({ }, use) => {
        await use(new CollectionPage());
    },

    sideBarComponent: async ({ }, use) => {
        await use(new SideBarComponent());
    },


    photographerWorkflow: async ({ loginPage, homePage, photoDetailPage, photographerProfilePage }, use
    ) => {
        await use(
            new PhotographerProfileWorkflow(
                loginPage,
                homePage,
                photoDetailPage,
                photographerProfilePage
            )
        );
    },

    userProfileWorkflow: async ({ loginPage, homePage, sideBarComponent, myProfilePage, editProfilePage },use
    ) => {
        await use(
            new UserProfileWorkflow(
                loginPage,
                homePage,
                sideBarComponent,
                myProfilePage,
                editProfilePage
            )
        );
    },

    collectionWorkflow: async ({ loginPage, homePage, collectionPage }, use) => {
        await use(new CollectionWorkflow(loginPage, homePage, collectionPage));
    },
});

export { expect };