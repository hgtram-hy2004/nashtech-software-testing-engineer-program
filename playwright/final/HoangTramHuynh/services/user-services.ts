import { APIResponse } from '@playwright/test';
import { API_ENDPOINT, ACCESS_TOKEN } from '../constant/url';
import { APIUtils } from '../core/api/api';

export class UserServices {
    private static getHeaders(): Record<string, string> {
        if (!ACCESS_TOKEN) {
            throw new Error('Access token is missing.');
        }
        return {
            'Content-Type': 'application/json',
            Authorization: `Bearer ${ACCESS_TOKEN}`,
        };
    }

    static async updateUsername(username: string): Promise<APIResponse> {
        return await APIUtils.put(`${API_ENDPOINT}/me`, {
            headers: this.getHeaders(),
            data: { username, },
        });
    }
}