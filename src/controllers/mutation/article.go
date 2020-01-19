package mutation

import (
	"github.com/bmwadforth/back/src/controllers/types"
	"github.com/bmwadforth/back/src/database"
	"github.com/graphql-go/graphql"
)

var ArticleMutation = &graphql.Field{
	Args: graphql.FieldConfigArgument{
		"title": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
		"description": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
		"data": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
		"author": &graphql.ArgumentConfig{
			Type: graphql.Int,
		},
		"tags": &graphql.ArgumentConfig{
			Type: graphql.NewList(graphql.String),
		},
	},
	Type: types.AuthorType,
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		//TODO: Sort out authorization
		/*
		token := p.Context.Value("token").(string)
		_, err := service.ValidateToken(token); if err != nil {
			return nil, errors.New("unauthorized")
		}*/

		title, _ := p.Args["title"].(string)
		description, _ := p.Args["description"].(string)
		data, _ := p.Args["data"].(string)
		var tags []string
		uncleanTags, _ := p.Args["tags"].([]interface{})
		for _, e := range uncleanTags  {
			tags = append(tags, e.(string))
		}
		author, _ := p.Args["author"].(int)

		//TODO: Validate user jwt, etc.

		err := database.NewArticle(title, description, []byte(data), tags, author)
		if err != nil {
			return nil, err
		}

		return nil, nil
	},
}
