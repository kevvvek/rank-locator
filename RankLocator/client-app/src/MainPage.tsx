import React, { useState } from "react";
import RankApi from "./api/RankApi";

const MainPage = () => {

    const [engine, setEngine] = useState("Google");
    const [keyword, setKeyWord] = useState("");
    const [url, setUrl] = useState("");
    const [ranks, setRanks] = useState<number[]>([]);
    const [message, setMessage] = useState("");
    const [isLoading, setIsLoading] = useState(false);

    const handleSearch = () => {
        // validate input
        if (!keyword) { setMessage("Please input keyword"); return; }
        if (!url) { setMessage("Please input URL"); return; }

        setIsLoading(true);
        setRanks([]);
        setMessage("");

        RankApi.search(engine, keyword, url).then(response => {
            setIsLoading(false);

            if (response.isSuccess) {
                setRanks(response.ranks);
                if (response.ranks.length == 0) {
                    setMessage("URL not found in the search results.");
                }
            } else {
                setMessage(response.message);
            }
        });
    }

    return (
        <div className="container">
            <div className="card mt-3 mb-3">
                <div className="card-body">
                    <h1>Rank Locator</h1>
                    <p>A smart tool to locate the rank of your URL in search engine.</p>

                    <div className="mb-3">
                        <div className="form-label">Keyword</div>
                        <input className="form-control" onChange={e => setKeyWord(e.target.value)} />
                    </div>

                    <div className="mb-3">
                        <div className="form-label">URL</div>
                        <input className="form-control" onChange={e => setUrl(e.target.value)} />
                    </div>

                    <button className="btn btn-primary" onClick={handleSearch} disabled={isLoading}>Search</button>
                </div>
            </div>

            {isLoading ?
                <div className="progress">
                    <div className="progress-bar progress-bar-striped progress-bar-animated" role="progressbar" style={{ width: "100%" }}></div>
                </div>
                : null}

            {ranks.length > 0 ? <div>
                <h2>Rank</h2>
                <p>{ranks.join(", ")}</p>
            </div> : null}

            {message ? <div>{message}</div> : null}
        </div>
    );
}

export default MainPage;