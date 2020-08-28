import express from 'express';
import buildResponse from '../util/response';
import { Project } from '../data';
import axios from 'axios';

const router = express.Router();

router.get('/', async (req, res, next) => {
  try {
    const projectsToReturn = [];
    const projects = await Project.findAll({});
    for (let project of projects) {
      project = project.toJSON();
      //["https:/", "/bmwadforth/<repo>"]
      const repoPath = project.repository.split('/github.com');
      const res = await axios.get(`https://api.github.com/repos${repoPath[1]}/commits/master`, {
        auth: {
          username: 'bmwadforth',
          password: process.env.BMWADFORTH_GITHUB_TOKEN,
        },
      });

      const data = res.data;
      project.githubMeta = {
        hash: data.sha,
        stats: data.stats,
      };

      projectsToReturn.push(project);
    }
    res.json(buildResponse('Fetched projects successfully', projectsToReturn));
  } catch (err) {
    next(err);
  }
});

export default router;
