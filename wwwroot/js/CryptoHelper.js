// 1. Generate RSA-OAEP Key Pair (2048-bit)
async function generateKeyPair() {
    return await window.crypto.subtle.generateKey(
        {
            name: "RSA-OAEP",
            modulusLength: 2048,
            publicExponent: new Uint8Array([1, 0, 1]),
            hash: "SHA-256",
        },
        true, // keys are exportable
        ["encrypt", "decrypt"]
    );
};

// 2. Export a key to a Base64 String (to save in DB or localStorage)
async function exportKey(key) {
    const type = key.type === "public" ? "spki" : "pkcs8";
    const exported = await window.crypto.subtle.exportKey(type, key);
    return btoa(String.fromCharCode(...new Uint8Array(exported)));
};

// 3. Encrypt Plaintext using a Public Key String
async function encryptMessage(publicKeyBase64, plaintext) {
    // Convert Base64 back to a format the browser understands (ArrayBuffer)
    const binaryDerString = window.atob(publicKeyBase64);
    const binaryDer = new Uint8Array(binaryDerString.length);
    for (let i = 0; i < binaryDerString.length; i++) {
        binaryDer[i] = binaryDerString.charCodeAt(i);
    }

    const publicKey = await window.crypto.subtle.importKey(
        "spki",
        binaryDer.buffer,
        { name: "RSA-OAEP", hash: "SHA-256" },
        true,
        ["encrypt"]
    );

    const encoder = new TextEncoder();
    const data = encoder.encode(plaintext);

    const encrypted = await window.crypto.subtle.encrypt(
        { name: "RSA-OAEP" },
        publicKey,
        data
    );

    // Return as Base64 so C# can save it as a string
    return btoa(String.fromCharCode(...new Uint8Array(encrypted)));
};

// 4. Decrypt Ciphertext using a Private Key String
async function decryptMessage(privateKeyBase64, ciphertextBase64) {
    // 1. Sanitize: Restore '+' signs that might have been lost in transmission
    const sanitizedCiphertext = ciphertextBase64.replace(/ /g, '+');

    // 2. Decode Private Key from Base64 to Uint8Array
    const binaryPrivString = window.atob(privateKeyBase64);
    const binaryPriv = new Uint8Array(binaryPrivString.length);
    for (let i = 0; i < binaryPrivString.length; i++) {
        binaryPriv[i] = binaryPrivString.charCodeAt(i);
    }

    // 3. Import the Private Key
    const privateKey = await window.crypto.subtle.importKey(
        "pkcs8",
        binaryPriv.buffer,
        { name: "RSA-OAEP", hash: "SHA-256" },
        true,
        ["decrypt"]
    );

    // 4. Decode the SANITIZED Ciphertext from Base64 to Uint8Array
    const binaryCipherString = window.atob(sanitizedCiphertext);
    const binaryCipher = new Uint8Array(binaryCipherString.length);
    for (let i = 0; i < binaryCipherString.length; i++) {
        binaryCipher[i] = binaryCipherString.charCodeAt(i);
    }

    // 5. Decrypt
    const decrypted = await window.crypto.subtle.decrypt(
        { name: "RSA-OAEP" },
        privateKey,
        binaryCipher.buffer
    );

    // 6. Return as readable text
    return new TextDecoder().decode(decrypted);
}