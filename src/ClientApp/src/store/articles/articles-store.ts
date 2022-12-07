import {atom, selector, selectorFamily} from 'recoil';
import axiosClient from '../apiClient';
import {IApiResponse} from '../base';

export interface IArticle {
    articleId: number;
    title: string;
    description: string;
    thumbnailId: string;
    contentId: string;
    createdDate: Date;
    updatedDate: Date;
    thumbnailDataUrl: string;
    contentDataUrl: string;
    content?: string;
}

export interface IArticlesState extends IApiResponse<IArticle[]> {
}

export interface IArticleState extends IApiResponse<IArticle> {
}

export const articlesStateSelector = selector<IArticlesState>({
    key: 'articlesStateSelector',
    get: async ({get}) => {
        try {
            const response = await axiosClient.get<IArticlesState>('/article');

            return response.data as IArticlesState;
        } catch (error) {
            throw error;
        }
    },
    set: ({set}, newVal) => set(articlesState, newVal as IArticlesState)
});

const articlesState = atom<IArticlesState>({
    key: 'articlesState',
    default: {message: '', payload: undefined, errors: undefined} as IArticlesState
});

export const articleStateSelector = selectorFamily<IArticleState, string>({
    key: 'articlesStateSelector',
    get: (articleId: string) => async ({get}) => {
        try {
            const response = await axiosClient.get<IArticleState>(`/article/${articleId}`);

            const { payload } = response.data;
            const contentUrl = payload?.contentDataUrl;
            if (contentUrl) {
                const contentResponse = await axiosClient.get(contentUrl);
                payload!.content = contentResponse.data;
            }

            return response.data as IArticleState;
        } catch (error) {
            throw error;
        }
    }
});

const articleState = atom<IArticleState>({
    key: 'articleState',
    default: {message: '', payload: undefined, errors: undefined} as IArticleState
});

export default articlesState;