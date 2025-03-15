require('dotenv').config();
const express = require('express');
const cors = require('cors');
const mongoose = require('mongoose');
const admin = require('firebase-admin');
const stripe = require('stripe')(process.env.STRIPE_SECRET_KEY);

const app = express();
app.use(cors());
app.use(express.json());

// Initialize Firebase Admin SDK
const serviceAccount = require("./firebase-service-account.json");
admin.initializeApp({
    credential: admin.credential.cert(serviceAccount),
    databaseURL: process.env.FIREBASE_DB_URL
});

// Connect to MongoDB
mongoose.connect(process.env.MONGO_URI, { useNewUrlParser: true, useUnifiedTopology: true })
    .then(() => console.log("MongoDB Connected"))
    .catch(err => console.log(err));

// User Schema (MongoDB)
const UserSchema = new mongoose.Schema({
    firebaseUID: String,
    username: String,
    vrCoins: { type: Number, default: 0 },
    isBanned: { type: Boolean, default: false }
});
const User = mongoose.model("User", UserSchema);

// **User Authentication**
app.post('/login', async (req, res) => {
    const { token } = req.body;
    try {
        const decodedToken = await admin.auth().verifyIdToken(token);
        let user = await User.findOne({ firebaseUID: decodedToken.uid });

        if (!user) {
            user = new User({ firebaseUID: decodedToken.uid, username: decodedToken.name });
            await user.save();
        }

        res.json({ success: true, user });
    } catch (error) {
        res.status(401).json({ success: false, error: "Unauthorized" });
    }
});

// **VRCoins Payment System (Stripe)**
app.post('/buy-coins', async (req, res) => {
    const { userId, amount } = req.body;
    try {
        const session = await stripe.checkout.sessions.create({
            payment_method_types: ['card'],
            line_items: [{
                price_data: {
                    currency: 'usd',
                    product_data: { name: 'VRCoins' },
                    unit_amount: amount * 100,
                },
                quantity: 1,
            }],
            mode: 'payment',
            success_url: 'https://yourgame.com/success',
            cancel_url: 'https://yourgame.com/cancel',
        });

        res.json({ id: session.id });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// **AI Avatar Generation (External API Call)**
app.post('/generate-avatar', async (req, res) => {
    const { userId, imageUrl } = req.body;
    try {
        const aiResponse = await fetch("https://your-ai-avatar-api.com/generate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ image: imageUrl })
        });
        const avatarData = await aiResponse.json();
        res.json({ success: true, avatar: avatarData });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// **AI World Generation**
app.post('/generate-world', async (req, res) => {
    const { userId, description } = req.body;
    try {
        const aiResponse = await fetch("https://your-ai-world-api.com/generate", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ description })
        });
        const worldData = await aiResponse.json();
        res.json({ success: true, world: worldData });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// **Anti-Cheat System (Detect Cheaters)**
app.post('/report-cheat', async (req, res) => {
    const { userId, reason } = req.body;
    try {
        const user = await User.findById(userId);
        if (user) {
            user.isBanned = true;
            await user.save();
            res.json({ success: true, message: "User has been banned." });
        } else {
            res.status(404).json({ error: "User not found." });
        }
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// **Get User Profile**
app.get('/profile/:userId', async (req, res) => {
    try {
        const user = await User.findById(req.params.userId);
        res.json(user);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

app.listen(5000, () => console.log("Backend Server Running on Port 5000"));
