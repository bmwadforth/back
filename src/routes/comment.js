import express from 'express';
import buildResponse from '../util/response';
import { Comment } from '../data';

const router = express.Router();

router.get('/', async (req, res, next) => {
  const comments = await Comment.findAll();
  res.json(buildResponse('Fetched articles successfully', comments));
});

router.put('/', async (req, res, next) => {
  try {
    const { comment } = req.body;
    const { article } = req.query;
    // TODO: Validate request body
    await Comment.create({ ip: req.connection.remoteAddress, comment, articleId: article });
    res.json(buildResponse('Comment created successfully', null));
  } catch (err) {
    next(err);
  }
});

export default router;
