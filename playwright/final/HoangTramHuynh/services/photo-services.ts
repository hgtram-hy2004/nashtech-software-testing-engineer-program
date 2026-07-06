import { APIResponse } from '@playwright/test';
import { API_ENDPOINT, ACCESS_TOKEN } from '../constant/url';
import { APIUtils } from '../core/api/api';

export class PhotoServices {
    private static getHeaders(): Record<string, string> {
        if (!ACCESS_TOKEN) {
            throw new Error('Access token is missing.');
        }

        return {
            Authorization: `Bearer ${ACCESS_TOKEN}`,
            'Content-Type': 'application/json',
        };
    }

    static async getRandomPhotos(count: number): Promise<APIResponse> {
        return await APIUtils.get(`${API_ENDPOINT}/photos/random`, {
            headers: this.getHeaders(),
            params: { count, },
        });
    }
}