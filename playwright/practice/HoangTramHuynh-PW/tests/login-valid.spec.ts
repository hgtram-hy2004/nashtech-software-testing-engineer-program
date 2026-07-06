import { test } from '../core/fixture/base-fixture';
import loginData from '../test-data/login-data.json';

test.describe('Login Function', () => {
  test('Verify that the user can login with valid account successfully', async ({
    loginPage,
    homePage,
  }) => {
    await test.step('Given the user visits the TMS website', async () => {
      await loginPage.openLoginPage();
      await loginPage.verifyLoginPageIsDisplayed();
    });

    await test.step('When the user inputs a valid account', async () => {
      await loginPage.login(
        loginData.validAdminAccount.username,
        loginData.validAdminAccount.password
      );
    });

     await test.step('Then the user is logged into the system successfully', async () => {
        await homePage.verifyHomePageIsDisplayed();
        await homePage.verifyLoginWithCorrectAccount(loginData.validAdminAccount.username);
    });
  });
});