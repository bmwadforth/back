import { DataTypes, Model } from 'sequelize';
import sequelize from './index';

class Article extends Model {}

Article.init(
  {
    // Model attributes are defined here
    title: {
      type: DataTypes.STRING,
      allowNull: false,
      unique: true,
    },
    description: {
      type: DataTypes.STRING,
      allowNull: false,
    },
    tags: {
      type: DataTypes.ARRAY(DataTypes.STRING(128)),
    },
    image: {
      type: DataTypes.STRING,
    },
  },
  {
    // Other model options go here
    sequelize,
    modelName: 'Article',
  }
);

Article.sync({ force: true });

export default Article;
