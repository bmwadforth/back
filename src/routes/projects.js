import express from 'express';
import buildResponse from '../util/response';
import { Article, Comment, Project } from '../data';
import { authorize } from '../middleware/authorization';

const router = express.Router();

router.get('/', async (req, res, next) => {
  try {
    const projects = await Project.findAll({});
    res.json(buildResponse('Fetched projects successfully', projects));
  } catch (err) {
    next(err);
  }
});

export default router;
