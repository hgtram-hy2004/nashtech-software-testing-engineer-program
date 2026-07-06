import { test as base, expect } from '@playwright/test';
import { LoginPage } from '../../page-object/login-page';
import { HomePage } from '../../page-object/home-page';

type PageFixtures = {
  loginPage: LoginPage;
  homePage: HomePage;
};

export const test = base.extend<PageFixtures>({
  loginPage: async ({ page }, use) => {
    const loginPage = new LoginPage(page);
    await use(loginPage);
  },

  homePage: async ({ page }, use) => {
    const homePage = new HomePage(page);
    await use(homePage);
  },
});

export { expect };