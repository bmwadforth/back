import {articleStateSelector, IArticleState} from "../store/articles/articles-store";
import {useRecoilValueLoadable} from "recoil";
import {useEffect} from "react";

export default function useArticle(articleId: string): [boolean, IArticleState] {
    const article = useRecoilValueLoadable(articleStateSelector(articleId));

    useEffect(() => {
        if (article.state === "hasError") {
            throw new Error(article.contents.error);
        }
    }, [article]);

    return [article.state === "loading", article.contents];
}