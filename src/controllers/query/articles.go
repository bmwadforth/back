package query

import (
	"errors"
	"github.com/bmwadforth/back/src/controllers/types"
	"github.com/bmwadforth/back/src/database"
	"github.com/graphql-go/graphql"
)

var ArticlesQuery = &graphql.Field{
	Type: graphql.NewList(types.ArticleType),
	Args: graphql.FieldConfigArgument{
		"id": &graphql.ArgumentConfig{
			Type: graphql.Int,
		},
	},
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
	},
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		//Get Article
		return nil, nil
	},
}
