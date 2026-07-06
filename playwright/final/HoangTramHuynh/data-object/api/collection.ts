export interface CreateCollectionResponse {
    id: string;
    title: string;
    description?: string;
    total_photos: number;
    private: boolean;
}