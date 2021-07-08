
var BaseUrl = 'http://localhost:816/api/game';


const fetchAPI = async (url, postData) => {
    try {
        const response = await fetch(`${BaseUrl}${url}`, {
            method: 'POST',
            body: JSON.stringify(postData),
            headers: {
                'Content-Type': 'application/json'
            }
        });
        const data = await response.json();
        console.log('***', data.data);
        return data.data;
    } catch (error) {
        console.log(error);
    }
}

class GamePlay {

    Initialize(_GameSessionID) {
        this.GameSessionID = _GameSessionID
    }

    onLevelComplete(_CurrentLevel, _CurrentScore) {
        var postData = {
            GameSessionID: this.GameSessionID,
            CurrentLevel: _CurrentLevel,
            CurrentScore: _CurrentScore
        }

        fetchAPI('/gLevelCompl', postData);
    }

    async onBonusRewardRequest(_CurrentLevel) {
        var postData = {
            GameSessionID: this.GameSessionID,
            CurrentLevel: _CurrentLevel,
        }
        var data = await fetchAPI('/gBonusReward', postData);

        let obj = {
            WinnerMessage: `You Won ${data.RewardItem.RewardName} Reward`,
            NextRewardLevel: data.NextRewadLevel,
            RewardID: data.RewardItem.EngagementRewardId
        };
        return obj;
    }

    onBonusAwarded(_RewardID) {
        var postData = {
            GameSessionID: this.GameSessionID,
            RewardID: _RewardID,
        }

        fetchAPI(`/gBonusAwarded`, postData);
    }

    onGameReplay() {
        fetch(`${BaseUrl}/gameReplay?GameSessionID=${this.GameSessionID}`)
            .then(response => {
                response.json().then(data => {
                    console.log('***', data.data);
                })
            }).catch(error => {
                console.log(error);
            });
    }
}


