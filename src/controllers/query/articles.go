package query

import (
	"errors"
	"github.com/bmwadforth/back/src/controllers/types"
	"github.com/bmwadforth/back/src/database"
	"github.com/graphql-go/graphql"
)

var ArticlesQuery = &graphql.Field{
	Type: graphql.NewList(types.ArticleType),
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		articles, err := database.GetArticles()
		if err != nil {
			return nil, errors.New("error fetching articles from database")
		}

		return articles, nil
	},
}

var ArticleQuery = &graphql.Field{
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
