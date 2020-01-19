package mutation

import (
	"errors"
	"github.com/bmwadforth/back/src/controllers/types"
	"github.com/bmwadforth/back/src/database"
	"github.com/bmwadforth/back/src/service"
	"github.com/graphql-go/graphql"
)

var AuthorMutation = &graphql.Field{
	Args: graphql.FieldConfigArgument{
		"firstName": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
		"lastName": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
		"username": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
		"password": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
	},
	Type: types.AuthorType,
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		firstName, _ := p.Args["firstName"].(string)
		lastName, _ := p.Args["lastName"].(string)
		username, _ := p.Args["username"].(string)
		password, _ := p.Args["password"].(string)

		hashedPassword := service.HashPassword([]byte(password))

		err := database.NewAuthor(firstName, lastName, username, hashedPassword); if err != nil {
			return nil, err
		}

		return nil, nil
	},
}

var LoginAuthorMutation = &graphql.Field{
	Args: graphql.FieldConfigArgument{
		"username": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
		"password": &graphql.ArgumentConfig{
			Type: graphql.String,
		},
	},
	Type: types.AuthorType,
	Resolve: func(p graphql.ResolveParams) (interface{}, error) {
		username, _ := p.Args["username"].(string)
		password, _ := p.Args["password"].(string)

		author, err := database.GetAuthor(username); if err != nil {
			return nil, err
		}

		validPassword := service.ValidatePasswordHash(string(author.Password), []byte(password))
		if !validPassword {
			return nil, errors.New("authentication challenge failed")
		}

		token, err := service.NewToken(author); if err != nil {
			return nil, errors.New("unable to generate authentication token")
		}

		return map[string]string{"token": string(token)}, nil
	},
}