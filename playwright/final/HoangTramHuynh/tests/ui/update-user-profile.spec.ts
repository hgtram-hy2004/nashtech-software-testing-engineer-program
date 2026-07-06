import { test } from '../../fixture/page-fixture';
import { CSVHelper } from '../../core/utils/csv';
import { StringUtils } from '../../core/utils/string';
import { AccountData } from '../../data-object/ui/account';
import { UserProfileData } from '../../data-object/ui/user-profile';

const accountInfos: AccountData[] = CSVHelper.readCSVFile('test-data/account-data.csv');
const userProfileInfos: UserProfileData[] = CSVHelper.readCSVFile('test-data/profile-data.csv');

for (const userProfileInfo of userProfileInfos) {
    test(`Verify update username successfully with account ${userProfileInfo.accountName}`, async ({ userProfileWorkflow,
    }) => {
        const accountInfo = accountInfos.find(account => account.accountName === userProfileInfo.accountName);
        if (!accountInfo) {
            throw new Error(`Account ${userProfileInfo.accountName} is not found in account-data.csv`);
        }
        const newUsername = StringUtils.generateRandomUsername(userProfileInfo.usernamePrefix);
        await userProfileWorkflow.login(accountInfo);
        await userProfileWorkflow.updateUsername(newUsername);

        await userProfileWorkflow.verifyUpdatedProfileIsDisplayed(newUsername, userProfileInfo.fullName);
        await userProfileWorkflow.restoreUsernameByAPI(accountInfo.username);

    });
}