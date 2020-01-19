package query

import "github.com/graphql-go/graphql"

var QueryType = graphql.NewObject(
	graphql.ObjectConfig{
		Name: "query",
		Fields: graphql.Fields{
			"articles": ArticlesQuery,
			"article": ArticleQuery,
			"projects": ProjectsQuery,
		},
	})
