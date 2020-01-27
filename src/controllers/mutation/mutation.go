package mutation

import "github.com/graphql-go/graphql"

var MutationType = graphql.NewObject(
	graphql.ObjectConfig{
		Name: "mutation",
		Fields: graphql.Fields{
			"author":  AuthorMutation,
			"login":   LoginAuthorMutation,
			"new_article": ArticleMutation,
		},
	})
