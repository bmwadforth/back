package query

import (
	"errors"
	"github.com/bmwadforth/back/src/controllers/types"
	"github.com/bmwadforth/back/src/database"
	"github.com/graphql-go/graphql"
)

var ArticlesQuery = &graphql.Field{
	Name: "Articles",
	Type: graphql.NewList(types.ArticleType),
	Args: graphql.FieldConfigArgument{
		"keywords": &graphql.ArgumentConfig{
			Type: graphql.NewList(graphql.String),
		},
	},
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		keywords, _ := p.Args["keywords"].([]interface{})

		if keywords != nil {
			var cleanedKeywords []string
			for _, e := range keywords {
				cleanedKeywords = append(cleanedKeywords, e.(string))
			}

			articles, err := database.SearchArticles(cleanedKeywords)
			if err != nil {
				return nil, errors.New("error searching articles")
			}

			return articles, nil
		} else {
			articles, err := database.GetArticles()
			if err != nil {
				return nil, errors.New("error fetching articles")
			}

			return articles, nil
		}
	},
}

var ArticleQuery = &graphql.Field{
	Name: "Article",
	Type: types.ArticleType,
	Args: graphql.FieldConfigArgument{
		"id": &graphql.ArgumentConfig{
			Type: graphql.Int,
		},
	},
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		id, _ := p.Args["id"].(int)
		article, err := database.GetArticle(id)
		if err != nil {
			return nil, err
		}
		return article, nil
	},
}
