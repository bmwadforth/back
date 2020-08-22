import express from 'express';
import buildResponse from '../util/response';
import { Article, Comment } from '../data';

const router = express.Router();

router.get('/', async (req, res, next) => {
  try {
    const comments = await Comment.findAll();
    res.json(buildResponse('Fetched comments successfully', comments));
  } catch (err) {
    next(err);
  }
});

router.put('/', async (req, res, next) => {
  try {
    const { comment } = req.body;
    const { article } = req.query;
    await Comment.create({ ip: req.connection.remoteAddress, comment, articleId: article });
    res.json(buildResponse('Comment created successfully', null));
  } catch (err) {
    next(err);
  }
});

export default router;
