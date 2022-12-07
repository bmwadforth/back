import {articlesStateSelector, IArticle, IArticlesState} from "../store/articles/articles-store";
import {useRecoilValueLoadable} from "recoil";
import {useEffect} from "react";

export default function useArticles(): [boolean, IArticlesState] {
    const articles = useRecoilValueLoadable(articlesStateSelector);

    useEffect(() => {
        if (articles.state === "hasError") {
            throw new Error(articles.contents.error);
        }
    }, [articles]);

    return [articles.state === "loading", articles.contents];
}