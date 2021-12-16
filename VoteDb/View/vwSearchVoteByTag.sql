CREATE VIEW [dbo].[vwSearchVoteByTag]
	AS 
SELECT Question
	  ,STRING_AGG(Tag.[Text], ', ') AS Tags
	  --,Tag.[Text] AS Tag
FROM Vote
INNER JOIN Vote_Tag ON Vote.Id = Vote_Tag.Vote
INNER JOIN Tag ON Vote_Tag.Tag = Tag.Id
WHERE Vote_Tag.Vote = ANY(SELECT Vote FROM Vote_Tag 
					  INNER JOIN Tag ON Vote_Tag.Tag = Tag.Id
					  WHERE Tag.[Text] = 'Город')
--WHERE Tag.[Text] = 'Город'
GROUP BY Question
GO