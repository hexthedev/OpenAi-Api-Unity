# Gets the PR associated with a commit and then writes all of the commit messages to a changelog
# sys.argv[1] = reponame
# sys.argv[2] = SHA key of commit to test
# sys.argv[3] = path to change log

import json
import sys
import requests

repo = sys.argv[1]
sha = sys.argv[2]
changelogPath = sys.argv[3]

# Get the pull request that's related to the commit
pulls_url = f"https://api.github.com/repos/hexthedev/{repo}/commits/{sha}/pulls"
pulls_headers = {
    'User-Agent' : 'hexthedev',
    'Accept' : 'application/vnd.github.groot-preview+json'
}

pulls_req = requests.get(pulls_url, headers = pulls_headers)

if pulls_req.status_code != 200:
    # If status fails, stop changelog creation
    raise Exception(f'Cannot retrieve pulls from commit:{sha} in repo:{repo}')

pulls_json = json.loads(pulls_req.text)

# Follow the commits link and get the commit messages in order
commits_url = pulls_json[0]["_links"]["commits"]["href"] 
commits_headers = {
    'User-Agent' : 'hexthedev'
}

commits_req = requests.get(commits_url, headers = commits_headers)

if commits_req.status_code != 200:
    raise Exception(f'Cannot get commit messages from {commits_url}')

commits_json = json.loads(commits_req.text)

# Write the changelog
commits_list = []
for commit in commits_json:
    commits_list.append(commit["commit"]["message"])

with open(changelogPath, 'w') as f:
    f.truncate(0)
    for com in commits_list:
        f.write(f'* {com}\n')