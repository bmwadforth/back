import React from 'react';
import { IArticle } from '../store/articles/articles-store';
import { Card, CardMedia, CardContent, Typography, CardActions, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import moment from "moment";

export interface IArticleTileProps {
    article: IArticle;
}

export default function ArticleTile({article}: IArticleTileProps) {
    const navigate = useNavigate();

    return (
        <Card sx={{ maxWidth: '100%' }}>
          <CardMedia
            component="img"
            height="140"
            image={article.thumbnailDataUrl}
            alt="Article thumbnail"
          />
          <CardContent>
              <Typography component="div" variant="h6">
                  {article.title}
              </Typography>
              <Typography gutterBottom variant="subtitle1" color="text.secondary" component="div">
                  {`${moment(article.createdDate).format('LLL')}`}
              </Typography>
              <Typography variant="body2" color="text.secondary">
                  {article.description}
              </Typography>
          </CardContent>
          <CardActions>
            <Button size="small" onClick={() => navigate(`/article/${article.articleId}`)}>Read</Button>
          </CardActions>
        </Card>
      );
}